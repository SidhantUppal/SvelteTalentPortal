<script>
	// @ts-nocheck

	import { onMount } from "svelte";
	import { writable } from "svelte/store";
	import { getTodayDsr, createDsr, submitDsr } from "../../api/dsr";
	import { formatDate, formatTimeToDate } from "../../utils/date";
	import { getProjects, getUsers } from "../../api/common";
    import BreadCrumb from "../../components/layout/breadCrumb.svelte";

	/**
	 * @type {any[]}
	 */
	var dsrs = writable([]);
	var error = false;
	var errorMesssage = "";
	let hashMap = new Map();
	let projects = [];
	let emails = [];
	let ccEmails = [];
	let isFormValid = writable(false);

	onMount(async () => {
		await fetchDsrs();
		await fetchProjects();
		await fetchUsers();
	});
	function removeRow(id) {
		dsrs.update((rows) => rows.filter((row) => row.id !== id));
	}
	async function fetchDsrs() {
		try {
			const res = await getTodayDsr();
			const data = res.data.data;
			let emptyUser = {
				id: 0,
				projectId: 0,
				description: "",
				fromTime: "",
				toTime: "",
				button: "Add",
			};
			data.push(emptyUser);
			dsrs.set(data);
			data.forEach((dsr) => {
				hashMap.set(dsr.id.toString(), dsr);
			});
		} catch (err) {
			error = true;
			errorMesssage = "Failed to fetch dsrs";
		}
	}
	async function fetchProjects() {
		try {
			const res = await getProjects();
			projects = res.data.data;
		} catch (err) {
			error = true;
			errorMesssage = "Failed to fetch projects";
		}
	}
	async function fetchUsers() {
		try {
			const res = await getUsers();
			emails = res.data.data;
			ccEmails = res.data.data;
		} catch (err) {
			error = true;
			errorMesssage = "Failed to fetch users";
		}
	}
	function saveDsr(id) {
		let idString = id.toString();
		let data = hashMap.get(idString);
		data.fromTime = formatTimeToDate(data.fromTime);
		data.toTime = formatTimeToDate(data.toTime);
		data.id = id;
		createDsr(data)
			.then((res) => {
				if (res.data.status) {
					removeRow(id);
					fetchDsrs();
				}
			})
			.catch((err) => {
				error = true;
				errorMesssage = "Failed to save dsr";
			});
	}

	const handleFieldChange = (e) => {
		const [name, id] = e.target.name.split("_");
		const value = e.target.value;
		hashMap.set(id, { ...hashMap.get(id), [name]: value });
	};

	$: if (hashMap.size > 0 && selectedEmail.length > 0) {
		isFormValid.set(true);
	} else {
		isFormValid.set(false);
	}

	// Function to handle form submission
	function handleSubmit(event) {
		event.preventDefault();
		let data = {
			dsrIds: Array.from(hashMap.keys()),
			senderIds: selectedEmail,
			senderCCIds: selectedEmailCC,
		};
		submitDsr(data)
			.then((res) => {
				if (res.data.status) {
					console.log("DSR submitted successfully");
				}
			})
			.catch((err) => {
				error = true;
				errorMesssage = "Failed to submit dsr";
			});
	}

	// Function to toggle visibility of CC dropdown
	let showDropdown = false;
	function toggleCC() {
		showDropdown = !showDropdown;
	}

	let selectedEmail = [];
	let selectedEmailCC = [];

	function toggleSelected(event) {
		const email = event.target.value;
		if (selectedEmail.includes(email)) {
			selectedEmail = selectedEmail.filter((item) => item !== email);
		} else {
			selectedEmail = [...selectedEmail, email];
		}
	}

	function toggleSelectedCC(event) {
		const email = event.target.value;
		if (selectedEmailCC.includes(email)) {
			selectedEmailCC = selectedEmailCC.filter((item) => item !== email);
		} else {
			selectedEmailCC = [...selectedEmailCC, email];
		}
	}
	function handleClickOutside(event) {
		if (
			!event.target.closest(".dropdown") &&
			!event.target.closest(".button-toggle")
		) {
			showDropdown = false;
		} else {
			showDropdown = true;
		}
	}

	document.body.addEventListener("click", handleClickOutside);
</script>

<svelte:head>
	<title>Add Dsr</title>
	<meta name="description" content="Dashboard" />
</svelte:head>
<div class="row">
	<div class="col-md-12">
		<BreadCrumb title = "Add Dsr" path={window.location.pathname}/>
	</div>
</div>
<div class="report-form">
	{#if error}
		<p class="error">{errorMesssage}</p>
	{:else}
		<table>
			<thead>
				<tr>
					<th>Project</th>
					<th class="text-center">Description</th>
					<th class="text-center">From Time</th>
					<th class="text-center">To Time</th>
					<th class="text-center">Action</th>
				</tr>
			</thead>
			<tbody>
				{#each $dsrs as dsr}
					<tr>
						<td>
							<select
								value={dsr.projectId}
								on:change={handleFieldChange}
								name="projectId_{dsr.id}"
								class="form-control"
							>
								<option value="0">Select Project</option>
								{#each projects as project}
									<option value={project.id}
										>{project.name}</option
									>
								{/each}
							</select>
						</td>
						<td class="text-center">
							<textarea
								type="text"
								class="form-control"
								name="description_{dsr.id}"
								value={dsr.description}
								on:change={(e) => handleFieldChange(e)}
							/>
						</td>
						<td class="text-center">
							<input
								type="time"
								class="form-control"
								name="fromTime_{dsr.id}"
								value={formatDate(dsr.fromTime)}
								on:change={(e) => handleFieldChange(e)}
							/>
						</td>
						<td class="text-center">
							<input
								type="time"
								class="form-control"
								name="toTime_{dsr.id}"
								value={formatDate(dsr.toTime)}
								on:change={(e) => handleFieldChange(e)}
							/>
						</td>
						<td class="text-center">
							<button
								class="primary"
								type="button"
								on:click={() => saveDsr(dsr.id)}
							>
								{dsr.button === "Add" ? "Save" : "Update"}
							</button>
						</td>
					</tr>
				{/each}
			</tbody>
		</table>
		<form on:submit={handleSubmit}>
			<div class="form-group">
				<label class="label" for="sendTo">Send To :</label>
				<div class="checkbox-group">
					{#each emails as { id, email }}
						<label class="checkbox-item">
							<input
								type="checkbox"
								value={id}
								on:change={toggleSelected}
								name="sendTo"
							/>{email}
						</label>
					{/each}
				</div>
			</div>

			<div class="form-group">
				<button class="button-toggle" type="button" on:click={toggleCC}
					>Add CC</button
				>
				<div class="dropdown" style="display: inline-block;">
					<div class="dropdown-content" class:show={showDropdown}>
						{#each ccEmails as { id, email }}
							<label class="dropdown-item">
								<input
									type="checkbox"
									value={id}
									on:change={toggleSelectedCC}
								/>
								{email}
							</label>
						{/each}
					</div>
				</div>
			</div>
			<button type="submit" class="primary" disabled={!$isFormValid}
				>Submit DSR</button
			>
		</form>
	{/if}
</div>

<style>
	table {
		width: 100%;
		border-collapse: collapse;
		margin-top: 1rem;
	}
	th,
	td {
		padding: 0.5rem;
		text-align: left;
		border: 1px solid #ddd;
	}

	th {
		background-color: #f2f2f2;
	}
	.label {
		font-weight: bold;
		margin-bottom: 8px;
		display: block;
		color: #333;
		text-align: left;
	}	
	.loader {
		display: flex;
		justify-content: center;
		align-items: center;
		height: 100vh;
	}
	.error {
		color: red;
		text-align: center;
		font-size: 1.5rem;
		margin-top: 1rem;
		justify-self: center;
	}
	.form-control {
		width: 80%;
		border: 1px solid #ccc;
		border-radius: 4px;
		padding: 6px 12px;
		margin: 5px 0;
		display: inline-block;
		box-sizing: border-box;
	}
	.primary {
		background-color: #05b1c5; /* cyan */
		border: none;
		color: white;
		padding: 10px 20px;
		text-align: center;
		text-decoration: none;
		display: inline-block;
		font-size: 16px;
		margin: 4px 2px;
		cursor: pointer;
		border-radius: 4px;
		text-align: center;
		text-decoration: solid;
	}
	.report-form {
		margin: 0 auto;
		padding: 20px;
		background-color: #f9f9f9;
		border-radius: 8px;
		box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
	}

	/* Styling for form elements */
	.form-group {
		margin-bottom: 20px;
	}

	label {
		font-weight: bold;
		margin-bottom: 5px;
		display: block;
		color: #333;
	}

	select,
	textarea {
		width: 100%;
		padding: 8px;
		border: 1px solid #ccc;
		border-radius: 4px;
		font-size: 16px;
	}

	.checkbox-group {
		display: flex;
		flex-wrap: wrap;
	}

	.checkbox-item {
		margin-right: 20px;
		color: #333;
	}

	.button-toggle {
		display: inline-block;
		margin-right: 10px;
	}

	.dropdown {
		position: relative;
		display: inline-block;
	}

	.dropdown-content {
		display: none;
		position: absolute;
		background-color: #fff;
		box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
		border-radius: 4px;

		z-index: 1;
	}

	.dropdown-content.show {
		display: block;
	}

	.dropdown-item {
		padding: 8px;
		cursor: pointer;
		color: #333;
	}

	.dropdown-item:hover {
		background-color: #f2f2f2;
	}

	button {
		background-color: #007bff;
		color: white;
		border: none;
		padding: 10px 20px;
		border-radius: 4px;
		font-size: 16px;
		cursor: pointer;
	}

	button:hover {
		background-color: #0056b3;
	}
</style>

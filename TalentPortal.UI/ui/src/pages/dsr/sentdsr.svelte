<script>
	// @ts-nocheck

	import { onDestroy, onMount } from "svelte";
	import { getDsrById, getDsrByLogId, getSentDsr } from "../../api/dsr";
	import Modal from "../../components/modal/Modal.svelte";
	import { writable } from "svelte/store";
    import BreadCrumb from "../../components/layout/breadCrumb.svelte";
	/**
	 * @type {any[]}
	 */
	let users = writable([]);
	var error = false;
	var errorMesssage = "";
	let dsrLogId = 0;
	let showModal = false;
	let showCommentModal = false;
	let dsrs = writable([]);
	let dialog = null;
	let commentDialog = null;
	$: if (showModal) {
		getDsr(dsrLogId);
	}
	$: if (commentDialog && !showCommentModal) {
		commentDialog.close();
	}
	$: if (dialog && !showModal) {
		dialog.close();
	}

	onMount(() => {
		getDsrs();
	});
	function getDsrs() {
		getSentDsr()
			.then((res) => {
				users.set(res.data.data);
			})
			.catch((err) => {
				error = true;
				errorMesssage = "Failed to fetch dsrs";
			});
	}
	async function getDsr(id) {
		try {
			const res = await getDsrByLogId(id);
			dsrs.set(res.data.data);
		} catch (err) {
			error = true;
			errorMesssage = "Failed to fetch dsr";
		}
	}
	const unsubscribe = users.subscribe((value) => {
		return value;
	});
	const unsubscribeDsr = dsrs.subscribe((value) => {
		return value;
	});
	onDestroy(() => {
		unsubscribe();
		unsubscribeDsr();
		if (dialog) dialog.close();
		if (commentDialog) commentDialog.close();
	});
</script>

<svelte:head>
	<title>Dsr List</title>
	<meta name="description" content="Dsr List" />
</svelte:head>
<div class="row">
	<div class="col-md-12">
		<BreadCrumb title = "Sent Dsr" path={window.location.pathname}/>
	</div>
</div>
<div class="table-container">
	{#if error}
		<p class="error">{errorMesssage}</p>
	{:else if users.length === 0}
		<div class="loader">Loading...</div>
	{:else}
		<table>
			<thead>
				<tr>
					<th>Project</th>
					<th class="text-center">Date</th>
					<th class="text-center">Status</th>
					<th class="text-center">Action</th>
				</tr>
			</thead>
			<tbody>
				{#each $users as user}
					<tr>
						<td class="td-text-center">
							<strong>
								{user.projectName}
							</strong>
							<small>{user.description}</small>
						</td>
						<td class="text-center"
							>{new Date(user.dateTime).toLocaleString()}</td
						> <td class="text-center">{user.status}</td>
						<td class="text-center">
							<button
								on:click={() => {
									dsrLogId = user.dsrLogId;
									showModal = true;
								}}
							>
								View
							</button>
						</td>
					</tr>
				{/each}
			</tbody>
		</table>
	{/if}
</div>
<Modal bind:showModal bind:dialog>
	<h2 slot="header">Dsr Details</h2>
	<div slot="body">
		{#if dsrs.length === 0}
			<div class="loader">Loading...</div>
		{:else}
			<table>
				<thead>
					<tr>
						<th>Project</th>
						<th class="text-center">Description</th>
						<th class="text-center">From Time</th>
						<th class="text-center">To Time</th>
					</tr>
				</thead>
				<tbody>
					{#each $dsrs as dsr}
						<tr>
							<td>{dsr.projectName}</td>
							<td class="text-center">{dsr.description}</td>
							<td class="text-center"
								>{new Date(dsr.fromTime).toLocaleString()}</td
							>
							<td class="text-center"
								>{new Date(dsr.toTime).toLocaleString()}</td
							>
						</tr>
					{/each}
				</tbody>
			</table>
		{/if}
	</div>
	<div slot="footer" class="footer">
		{#if $dsrs && $dsrs[0]?.senderEmails && Array.isArray($dsrs[0]?.senderEmails)}
			<div class="email-list">
				<label class="email-label" for="email-input"> To Emails </label>
				<ul class="emails">
					{#each $dsrs[0]?.senderEmails as email}
						<li>{email}</li>
					{/each}
				</ul>
			</div>
			<div class="email-list">
				<label class="email-label" for="email-input"> Cc Emails </label>
				<ul class="emails">
					{#each $dsrs[0]?.senderCCEmails as email}
						<li>{email}</li>
					{/each}
				</ul>
			</div>
			<div class="button-container">
				<button
					class="btn btn-secondary"
					type="button"
					on:click={() => (showCommentModal = true)}
					>Add Comment</button
				>
			</div>
		{/if}
	</div>
</Modal>
<Modal bind:showModal={showCommentModal} bind:dialog={commentDialog}>
	<h2 slot="header">Add Comment</h2>
	<div slot="body">
		<textarea
			class="form-control"
			name="comment"
			id="comment"
			cols="30"
			rows="10"
		></textarea>
	</div>
	<div slot="footer" class="footer">
		<button
			class="btn btn-secondary"
			type="button"
			on:click={() => (showCommentModal = false)}
		>
			Cancel
		</button>
		<button
			type="button"
			on:click={() => (showCommentModal = false)}
			class="btn btn-primary"
		>
			Save
		</button>
	</div>
</Modal>

<style>
	.email-list {
		background-color: white;
		padding: 20px;
		border-radius: 8px;
		box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
	}

	.email-label {
		font-size: 18px;
		font-weight: bold;
		color: black;
		margin-bottom: 10px;
		display: block;
	}

	.emails {
		list-style: none;
		padding: 0;
		margin: 0;
	}

	.emails li {
		font-size: 16px;
		margin-bottom: 5px;
		color: #333; /* Or any other color you prefer for emails */
	}
	.table-container {
		margin: 1rem 0;
		overflow-x: auto;
	}
	table {
		width: 100%;
		border-collapse: collapse;
		margin-top: 1rem;
	}
	.td-text-center {
		text-align: start;
		align-items: center;
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
	.footer {
		display: flex;
		justify-content: space-between;
	}
	.btn {
		padding: 0.5rem 1rem;
		border-radius: 0.3rem;
	}
	.btn-primary {
		background-color: #007bff;
		color: #fff;
	}
	.btn-secondary {
		background-color: #6c757d;
		color: #fff;
	}
	.button-container {
		display: flex;
		justify-content: flex-end;
		align-items: end;
	}
</style>

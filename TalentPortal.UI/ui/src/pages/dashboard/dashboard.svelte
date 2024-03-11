<script>
	// @ts-nocheck

	import ProjectCard from "../../components/ProjectCard.svelte";
	import Quote from "../../components/cards/quote.svelte";
	import Holiday from "../../components/cards/holiday.svelte";
	import WorkAnniversaryCard from "../../components/cards/WorkAnniversaryCard.svelte";
	import BirthdayCard from "../../components/cards/BirthdayCard.svelte";
	import BreadCrumb from "../../components/layout/breadCrumb.svelte";
	import { onMount } from "svelte";
	import { getAttendancetoday, timeIn, timeOut } from "../../api/dashboard";
	import { formatDate, formatDateToMinHrs } from "../../utils/date";
	import Modal from "../../components/modal/Modal.svelte";
	import {
		attendance,
		attendanceError,
		fetchAttendance,
		attendanceLoading,
	} from "../../lib/store/dashboard/dashboard";
	import Loader from "../../components/layout/loader.svelte";
	import NoData from "../../components/layout/noData.svelte";
	/**
	 * @type {any[]}
	 */
	let todayAttendance = {};
	let error = false;
	let errorMesssage = "";
	let userTimedIn = false;
	let alreadyTimedIn = false;
	let showModal = false;
	let dialog = null;
	onMount(async () => {
		await fetchAttendance();
	});
	attendance.subscribe((res) => {
		if (res.status) {
			let data = res.data;
			todayAttendance = data;
			if (data.timeIn != null && data.timeOut == null) {
				userTimedIn = true;
			} else if (data.timeIn != null && data.timeOut != null) {
				alreadyTimedIn = true;
			}
		} else {
			error = true;
			errorMesssage = "No data found";
		}
	});
	async function timeInUser() {
		const res = await timeIn();
		if (res.data.status) {
			await fetchAttendance();
		}
	}
	async function timeOutUser() {
		const res = await timeOut();
		if (!res.data.status) {
			if (res.data.data == false) {
				showModal = true;
				return;
			}
		}
		if (res.data.status) {
			await fetchAttendance();
		}
	}

	$: if (dialog && !showModal) {
		dialog.close();
	}
	$: if (dialog && !showModal) {
		dialog.close();
	}
</script>

<svelte:head>
	<title>Dashboard</title>
	<meta name="description" content="Dashboard" />
</svelte:head>
{#if $attendanceLoading}
	<Loader size="large" color="#007bff" />
{:else if $attendanceError}
	<NoData message={errorMesssage} />
{:else}
	<div class="row">
		<div class="col-md-6">
			<BreadCrumb title="Dashboard" path={window.location.pathname} />
		</div>
		<div class="col-md-6 d-flex justify-content-end">
			<div class="card text-center" style="width: 18rem;">
				<div class="card-body">
					{#if alreadyTimedIn}
						<h5 class="card-title">Total Working Hours</h5>
						<p class="card-text">
							{formatDateToMinHrs(
								todayAttendance.totalWorkingHours,
							)}
						</p>
					{:else if userTimedIn}
						<p class="card-text">
							Time In:
							{formatDate(todayAttendance.timeIn)}
						</p>
						<button
							class="btn btn-primary"
							type="button"
							on:click={() => {
								timeOutUser();
							}}
						>
							Time Out</button
						>
					{:else}
						<button
							class="btn btn-primary"
							type="button"
							on:click={() => {
								timeInUser();
							}}
						>
							Time In</button
						>
					{/if}
					<!-- <h5 class="card-title"></h5>
			  <p class="card-text"></p>
			  <button href="#" class="btn btn-primary">Go somewhere</button> -->
				</div>
			</div>
		</div>
	</div>
	<hr />
	<div class="row">
		<ProjectCard title="Total Assigned Projects" value={9} />
		<ProjectCard title="Total Leaves" value={42} />
		<ProjectCard title="Total Dsr Received" value={18} />
	</div>
	<hr />
	<div class="row">
		<Quote />
	</div>
	<hr />
	<div class="row">
		<Holiday />
	</div>
	<hr />
	<div class="row">
		<WorkAnniversaryCard />
	</div>
	<hr />
	<div class="row">
		<BirthdayCard />
	</div>
{/if}
<Modal bind:showModal bind:dialog>
	<h2 slot="header" class="text-center">DSR</h2>
	<div slot="body">
		<p class="text-center">Dsr not submitted for today</p>
	</div>
	<div slot="footer" class="footer">
		<button class="btn btn-secondary" on:click={() => (showModal = false)}>
			OK
		</button>
	</div>
</Modal>

<style>
	.row {
		display: flex;
		justify-content: space-between;
		background-color: #f2f2f2;
		border: 1px solid #ddd; /* Light gray border */
		border-radius: 10px; /* Set border radius */
	}
</style>

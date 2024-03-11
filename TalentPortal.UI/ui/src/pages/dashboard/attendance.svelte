<script>
    // @ts-nocheck
    import { onDestroy, onMount } from "svelte";
    import { writable } from "svelte/store";
    import { getAttendance, timeOut } from "../../api/dashboard";
    import * as animation from "svelte/easing";
    import BreadCrumb from "../../components/layout/breadCrumb.svelte";
    import { formatDateToMinHrs } from "../../utils/date";
    import NoData from "../../components/layout/noData.svelte";

    /**
     * @type {any[]}
     */
    let reports = writable([]);
    var error = false;
    var errorMesssage = "";
    let params = {
        id: 1,
        fromDate: "12-12-2021", // "yyyy-mm-dd"
        toDate: "12-12-2021", // "yyyy-mm-dd"
    };

    onMount(() => {
        bindData(params);
    });
    function bindData(params) {
        getAttendance(params)
            .then((res) => {
                if (res.data.status) {
                    reports.set(res.data.data);
                } else {
                    error = true;
                    errorMesssage = "No data found";
                }
            })
            .catch((err) => {
                error = true;
                errorMesssage = "Failed to fetch data";
            });
    }

    const unsubscribe = reports.subscribe((value) => {
        return value;
    });
    onDestroy(() => {
        unsubscribe();
    });
</script>

<svelte:head>
    <title>Attendance</title>
    <meta name="description" content="Attendance" />
</svelte:head>
<div class="row">
    <div class="col-md-12">
        <BreadCrumb title="Attendance" path={window.location.pathname} />
    </div>
</div>
<div class="table-container">
    {#if error}
        <NoData message={errorMesssage} />
    {:else if reports.length === 0}
        <div class="loader">Loading...</div>
    {:else}
        <table>
            <thead>
                <tr>
                    <th>Date</th>
                    <th class="text-center">Time In</th>
                    <th class="text-center">Time Out</th>
                    <th class="text-center">Total Working Hours</th>
                    <th class="text-center">Status</th>
                </tr>
            </thead>
            <tbody>
                {#each $reports as report}
                    <tr>
                        <td>
                            {new Date(
                                report.createdAt,
                            ).toLocaleDateString()}</td
                        >
                        <td class="text-center"
                            >{new Date(report.timeIn).toLocaleString()}</td
                        >
                        <td class="text-center"
                            >{report.timeOut
                                ? new Date(report.timeOut).toLocaleString()
                                : "-"}</td
                        >
                        <td class="text-center"
                            >{report.totalWorkingHours
                                ? formatDateToMinHrs(report.totalWorkingHours)
                                : "-"}</td
                        >
                        <td class="text-center">{report.status}</td>
                    </tr>
                {/each}
            </tbody>
        </table>
    {/if}
</div>

<style>
    .table-container {
        margin: 1rem 0;
        overflow-x: auto;
    }
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
</style>

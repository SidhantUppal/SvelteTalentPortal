<script>
    // @ts-nocheck

    import { onDestroy, onMount } from "svelte";
    import { getDsrById, getSentDsr } from "../../api/dsr";
    import Modal from "../../components/modal/Modal.svelte";
    import { writable } from "svelte/store";
    import { getReceivedReport, getReportById } from "../../api/report_api";
    import BreadCrumb from "../../components/layout/breadCrumb.svelte";
    /**
     * @type {any[]}
     */
    let reports = writable([]);
    var error = false;
    var errorMesssage = "";
    let dsrId = 0;
    let showModal = false;
    let dsrs = writable([]);
    let dialog = null;
    $: if (showModal) {
        getReport(dsrId);
    }
    $: if (dialog && !showModal) {
        dialog.close();
    }

    onMount(() => {
        bindData();
    });
    function bindData() {
        getReceivedReport()
            .then((res) => {
                reports.set(res.data.data);
            })
            .catch((err) => {
                error = true;
                errorMesssage = "Failed to fetch users";
            });
    }
    function getReport(id) {
        getReportById(id)
            .then((res) => {
                dsrs.set(res.data.data);
            })
            .catch((err) => {
                error = true;
                errorMesssage = "Failed to fetch dsr";
            });
    }
    const unsubscribe = reports.subscribe((value) => {
        return value;
    });
    const unsubscribeDsr = reports.subscribe((value) => {
        return value;
    });
    onDestroy(() => {
        unsubscribe();
        unsubscribeDsr();
        if (dialog) dialog.close();
    });
</script>

<svelte:head>
    <title>Sent Report</title>
    <meta name="description" content="Sent Report" />
</svelte:head>
<div class="row">
	<div class="col-md-12">
		<BreadCrumb title = "Received Report" path={window.location.pathname}/>
	</div>
</div>
<div class="table-container">
    {#if error}
        <p class="error">{errorMesssage}</p>
    {:else if reports.length === 0}
        <div class="loader">Loading...</div>
    {:else}
        <table>
            <thead>
                <tr>
                    <th>Project</th>
                    <th class="text-center">Date</th>
                    <th class="text-center">Description</th>
                    <th class="text-center">Action</th>
                </tr>
            </thead>
            <tbody>
                {#each $reports as report}
                    <tr>
                        <td>{report.projectName}</td>
                        <td class="text-center"
                            >{new Date(report.date).toLocaleString()}</td
                        >
                        <td class="text-center">{report.description}</td>
                        <td class="text-center">
                            <button
                                on:click={() => {
                                    dsrId = report.id;
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
    <h2 slot="header">Modal Header</h2>
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
        <button class="btn btn-secondary" on:click={() => (showModal = false)}>
            Cancel
        </button>
        <button on:click={() => (showModal = false)} class="btn btn-primary">
            Save
        </button>
    </div>
</Modal>

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
</style>

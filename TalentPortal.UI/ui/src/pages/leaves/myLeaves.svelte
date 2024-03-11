<script>
    // @ts-nocheck

    import { onDestroy, onMount } from "svelte";
    import { writable } from "svelte/store";
    import { getleaves } from "../../api/leaves";
    import BreadCrumb from "../../components/layout/breadCrumb.svelte";
    /**
     * @type {any[]}
     */
    let leaves = writable([]);
    var error = false;
    var errorMesssage = "";
    onMount(() => {
        bindData();
    });
    function bindData() {
        getleaves()
            .then((res) => {
                if (res.data.status) {
                    let data = res.data.data;
                    data.forEach((leave, index) => {
                        leave.sNo = index + 1;
                    });
                    leaves.set(data);
                } else {
                    error = true;
                    errorMesssage = "No data found";
                }
            })
            .catch((err) => {
                error = true;
                errorMesssage = "Failed to fetch users";
            });
    }

    const unsubscribe = leaves.subscribe((value) => {
        return value;
    });

    onDestroy(() => {
        unsubscribe();
    });
</script>

<svelte:head>
    <title>Sent Report</title>
    <meta name="description" content="Sent Report" />
</svelte:head>
<div class="row">
	<div class="col-md-12">
		<BreadCrumb title = "My Requests" path={window.location.pathname}/>
	</div>
</div>
<div class="table-container">
    {#if error}
        <p class="error">{errorMesssage}</p>
    {:else if leaves.length === 0}
        <div class="loader">Loading...</div>
    {:else}
        <table>
            <thead>
                <tr>
                    <th>S.No </th>
                    <th class="text-center">From </th>
                    <th class="text-center">To </th>
                    <th class="text-center">Shift</th>
                    <th class="text-center">Status</th>
                    <th class="text-center">Action</th>
                </tr>
            </thead>
            <tbody>
                {#each $leaves as leave}
                    <tr>
                        <td>{leave.sNo}</td>
                        <td class="text-center"
                            >{new Date(leave.fromDate).toLocaleDateString()}</td
                        >
                        <td class="text-center"
                            >{new Date(leave.toDate).toLocaleDateString()}</td
                        >
                        <td class="text-center">
                            {leave.shift}
                        </td>
                        <td class="text-center">
                            {leave.status}
                        </td>
                        <td class="text-center">
                            <button
                                disabled
                                type="button"
                                on:click={() => {
                                    // dsrId = user.id;
                                    // showModal = true;
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

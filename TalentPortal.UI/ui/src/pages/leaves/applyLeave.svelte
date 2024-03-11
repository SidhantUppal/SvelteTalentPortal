<!-- ApplyLeave.svelte -->
<script>
// @ts-nocheck

    import { onMount } from "svelte";
    import { getLeaveTypes, getUsers } from "../../api/common";
    import { writable } from "svelte/store";
    import { addLeave } from "../../api/leaves";
    import BreadCrumb from "../../components/layout/breadCrumb.svelte";

    let leaveTypeId = "";
    let fromDate = "";
    let toDate = "";
    let reason = "";
    let emails = [];
    let ccEmails = [];
    let leaveTypes = [];
    let error = false;
    let errorMesssage = "";
    const LeaveType = {
        FULL_DAY: 1,
        HALF_DAY: 2,
        SHORT_LEAVE: 3,
        WFH : 4
    };
    onMount(async () => {
        await fetchLeaveTypes();
        await fetchUsers();
    });
    async function fetchLeaveTypes() {
        try {
            const response = await getLeaveTypes();
            leaveTypes = response.data.data;
        } catch (error) {
            console.error("Failed to fetch leave types", error);
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
    // Function to handle form submission
    function handleSubmit(event) {
        event.preventDefault();
        const leaveDetails = {
            leaveTypeId,
            fromDate,
            toDate,
            reason,
            senderEmailIds: selectedEmail,
            senderCCEmailIds: selectedEmailCC,
        };
        addLeave(leaveDetails)
            .then((res) => {
                if (res.data.status) {
                    console.log("Leave added successfully");
                }
            })
            .catch((err) => {
                console.error("Failed to add leave", err);
            });
    }
    // Function to toggle visibility of CC dropdown
    let showDropdown = false;
    function toggleCC() {
        showDropdown = !showDropdown;
    }

    let selectedEmail = [];
    let selectedEmailCC = [];
    // @ts-ignore
    let isFormValid = writable(false);

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
    // @ts-ignore
    $: leaveTypeId 
    && fromDate
    && toDate
    && reason
    && selectedEmail.length > 0
    ? isFormValid.set(true)
    : isFormValid.set(false);

    $: console.log(isFormValid);

</script>
<div class="row">
	<div class="col-md-12">
		<BreadCrumb title = "Apply Leave" path={window.location.pathname}/>
	</div>
</div>
<div class="apply-leave-container">
    <!-- Leave Type dropdown -->
    <div class="row">
        <label for="leaveTypeId">Leave Type</label>
        <select id="leaveTypeId" 
        bind:value={leaveTypeId}
        >
            {#each leaveTypes as leave}
                <option value={leave.id}>{leave.name}</option>
            {/each}
        </select>

        <!-- Start Date -->
        <label for="fromDate">Start Date</label>
        <input type="date" id="fromDate" bind:value={fromDate} />

        <!-- End Date -->
        <label for="toDate">End Date</label>
        <input type="date" id="toDate" bind:value={toDate} />
    </div>

    <!-- Reason for leave -->
    <label for="reason">Reason for Leave</label>
    <textarea id="reason" rows="4" bind:value={reason}></textarea>
    <div class="form-group">
        <label for="sendTo">Send To</label>
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

    <!-- Submit button -->
    <div class="button-container">
        <button on:click={handleSubmit} disabled= {!isFormValid}>Apply</button>
    </div>
</div>

<style>
    .apply-leave-container {
        margin: 20px auto;
        padding: 20px;
        background-color: #f9f9f9;
        border-radius: 8px;
        box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    }

    label {
        font-weight: bold;
        margin-bottom: 8px;
        display: block;
        color: #333;
    }

    select,
    input[type="date"],
    textarea {
        width: calc(100% - 22px);
        padding: 8px;
        margin-bottom: 16px;
        border: 1px solid #ccc;
        border-radius: 4px;
    }

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

    .button-container {
        display: flex;
        justify-content: flex-end;
    }

    button {
        padding: 10px 20px;
        border: none;
        border-radius: 4px;
        background-color: #007bff;
        color: white;
        cursor: pointer;
    }

    button:hover {
        background-color: #0056b3;
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
    .row {
        display: flex;
        justify-content: space-between;
    }
    .row label {
        width: 30%;
    }
</style>

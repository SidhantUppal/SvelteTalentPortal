<!-- ReportForm.svelte -->
<script>
    import { getProjects, getUsers } from "../../api/common";
    import { onMount } from "svelte";
    import { createReport } from "../../api/report_api";
    import BreadCrumb from "../../components/layout/breadCrumb.svelte";

    let emails = [];
    let ccEmails = [];
    let projects = [];
    let error = false;
    let errorMesssage = "";
    let projectId = "";
    let description = "";

    onMount(async () => {
        await fetchProjects();
        await fetchUsers();
    });

    // Function to handle form submission
    function handleSubmit(event) {
        event.preventDefault();
        console.log("Form submitted");
        createReport({
            projectId,
            description,
            senderEmailIds: selectedEmail,
            senderCCEmailIds: selectedEmailCC,
        });
        // Logic to submit the report
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
<div class="row">
	<div class="col-md-12">
		<BreadCrumb title = "Create Report" path={window.location.pathname}/>
	</div>
</div>
<div class="report-form">
    <form on:submit={handleSubmit}>
        <div class="form-group">
            <label for="reportType">Report Type</label>
            <select id="reportType" bind:value={projectId}>
                {#each projects as project}
                    <option value={project.id}>{project.name}</option>
                {/each}
            </select>
        </div>

        <div class="form-group">
            <label for="description">Description</label>
            <textarea id="description" rows="4" bind:value={description}></textarea>
        </div>

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
            <button class="button-toggle" type="button" on:click={toggleCC}>Add CC</button>
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
        <button type="submit">Submit Report</button>
    </form>
</div>

<style>
    /* Styling for the form */
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

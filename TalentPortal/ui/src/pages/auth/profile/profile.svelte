<script>
    import { onMount } from "svelte";
    import { getUserProfile, profile } from "../../../lib/store/auth";

    let user = {};
    onMount(() => {
        getUserProfile();
    });

    profile.subscribe((res) => {
        console.log("res", res);
        user = res;
    });
</script>

<div>
    <h2 class="profile-header">Profile</h2>
    <form on:submit|preventDefault>
        <div class="form row">
            <div class="col-md-6">
                <div class="form-group">
                    <label for="username" class="form-label">Username:</label>
                    <input
                        type="text"
                        id="username"
                        class="form-control"
                        value={user?.userName}
                        disabled
                    />
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label for="firstname" class="form-label">Firstname:</label>
                    <input
                        type="text"
                        id="firstname"
                        class="form-control"
                        value={user?.firstName}
                        disabled
                        required
                    />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label for="lastname" class="form-label">Lastname:</label>
                    <input
                        type="text"
                        id="lastname"
                        class="form-control"
                        value={user?.lastName}
                        disabled
                        required
                    />
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label for="employeeCode" class="form-label"
                        >Employee Code:</label
                    >
                    <input
                        type="text"
                        id="employeeCode"
                        class="form-control"
                        value="12345"
                        disabled
                        required
                    />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label for="email" class="form-label">Email:</label>
                    <input
                        type="email"
                        id="email"
                        class="form-control"
                        value={user?.email}
                        disabled
                        required
                    />
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label for="department" class="form-label"
                        >Department:</label
                    >
                    <input
                        type="text"
                        id="department"
                        class="form-control"
                        value="Engineering"
                        disabled
                        required
                    />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label for="designation" class="form-label"
                        >Designation:</label
                    >
                    <input
                        type="text"
                        id="designation"
                        class="form-control"
                        value="Software Engineer"
                        disabled
                        required
                    />
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label for="mobile" class="form-label">Mobile:</label>
                    <input
                        type="text"
                        id="mobile"
                        class="form-control"
                        value="1234567890"
                        disabled
                        required
                    />
                </div>
            </div>
        </div>
        <div class="form-group">
            <label for="address" class="form-label">Address:</label>
            <textarea id="address" class="form-control" disabled required
                >123 Street, City, Country</textarea
            >
        </div>
    </form>
</div>

<style>
    .profile-container {
        margin: 0 auto;
        padding: 30px;
        border-radius: 10px;
        /* box-shadow: 0 0 20px rgba(0, 0, 0, 0.1); */
        /* background-color: #f0f0f0; */
    }

    .profile-header {
        text-align: center;
        color: #333;
        margin-bottom: 30px;
    }

    /* .profile-form {
      display: flex;
      flex-direction: column;
    } */
</style>

<script>
    // @ts-nocheck

    // @ts-ignore
    import { onDestroy, onMount } from "svelte";
    import { getDsrById } from "../../api/dsr";
    import { derived, writable } from "svelte/store";
    import Modal from "../../components/modal/Modal.svelte";

    var user = derived(user, ($user) => {
        return $user;
    });
</script>

<form class="row">
    <div class="form-group col-md-6">
        <label for="fromTime">From Time</label>
        <input
            type="time"
            class="form-control"
            id="fromTime"
            bind:value={$user.fromTime}
        />
    </div>
    <div class="form-group col-md-6">
        <label for="toTime">To Time</label>
        <input
            type="time"
            class="form-control"
            id="toTime"
            bind:value={$user.toTime}
        />
    </div>
</form>

<style>
    .row {
        margin: 0;
    }
    .form-group {
        margin-bottom: 1rem;
    }
    .form-control {
        width: 100%;
        padding: 0.375rem 0.75rem;
        font-size: 1rem;
        line-height: 1.5;
        color: #495057;
        background-color: #fff;
        background-clip: padding-box;
        border: 1px solid #ced4da;
        border-radius: 0.25rem;
        transition:
            border-color 0.15s ease-in-out,
            box-shadow 0.15s ease-in-out;
    }
</style>

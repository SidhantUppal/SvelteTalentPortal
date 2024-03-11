<script>
    import Navbar from "../components/layout/navbar.svelte";
    import { currentLayout } from "../lib/store/auth";
    import { sideBarToggle } from "../lib/store/uistore/_toggle";
    export let title = "Dashboard";
    
</script>

<svelte:head>
    <title>{title}</title>
</svelte:head>
<div class="container-fluid p-3">
    <div class="row">
        <Navbar />
        <main class="col-md-9 ms-sm-auto col-lg-10 px-md-4">
            <slot />
        </main>
    </div>
</div>

<script>
    import { writable } from 'svelte/store';
    import { onMount } from 'svelte';
    import { Router, Route } from 'svelte-routing';

    // Define writable store for routes
    export const routes = writable([]);

    // Function to register routes
    function registerRoute(path, component) {
        routes.update(existingRoutes => {
            return [...existingRoutes, { path, component }];
        });
    }

    onMount(() => {
        // Register routes on mount
        registerRoute('/sent-dsr', () => import('../pages/dsr/sentdsr.svelte'));
        // Add more routes as needed
    });
</script>

<Router>
    {#each $routes as { path, component }}
        <Route path={path} let:params>
            {#if component}
                {#await component() then cmp}
                    <svelte:component this={cmp.default} {...params} />
                {/await}
            {/if}
        </Route>
    {/each}
</Router>

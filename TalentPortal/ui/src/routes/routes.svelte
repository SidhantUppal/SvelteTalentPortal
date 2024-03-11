<script>
    // @ts-nocheck
    import { Router, Route } from "svelte-routing";
    import { routes } from "./routes";
    import Routeguard from "./routeguard.svelte";
    import { currentLayout } from "../lib/store/auth";
    import { navigate } from "svelte-routing";
    export let user = {};
    
    currentLayout.subscribe((val) => {
        let layout = val;
        if (layout === "dashboard") {
            navigate("/dashboard");
        } else {
            navigate("/auth/login");
        }
    });
</script>

<!-- svelte-ignore missing-declaration -->
<Router>
    {#if user}
        {#each routes as route}
            {#if !route.isPublic}
                <Routeguard {user}>
                    {#if route.hasChildren}
                        <Route path={route.path} component={route.component} />
                        {#each route.children as child}
                            <Route
                                path={child.path}
                                component={child.component}
                            />
                        {/each}
                    {:else}
                        {#if route.path == "/dashboard"}
                            <Route path="/" component={route.component} />
                        {/if}
                        <Route path={route.path} component={route.component} />
                    {/if}
                </Routeguard>
            {/if}
        {/each}
    {/if}
</Router>

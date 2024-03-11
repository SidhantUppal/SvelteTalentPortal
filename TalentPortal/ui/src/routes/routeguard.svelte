<script>
  import { Route, Router, navigate } from "svelte-routing";
  import { routes } from "./routes";
  export let user = {};
  // $: if (user && user.accessToken) {
  //   navigate("/dashboard"); // Redirect to dashboard
  // } else {
  //   navigate("/auth/login"); // Redirect to login page
  // }
</script>
{#if user && user.accessToken}
<!-- <Router >
  {#each routes as route}
    {#if !route.isPublic}
      <Route path={route.path} component={route.component} />
    {/if}
  {/each}
</Router> -->
  <slot />
{/if}

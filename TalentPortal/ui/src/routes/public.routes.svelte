<!-- public.routes.svelte -->
<script>
  // @ts-nocheck

  import { Router, Route } from "svelte-routing";
  import Dashboard from "../pages/dashboard/dashboard.svelte";
  import Error from "../components/error/urlerror.svelte";
  import Login from "../pages/auth/login/login.svelte";
  export let path = "";
</script>

<Router {path}>
  <Route path="/auth/login" component={Login} />
  <Route path="/*" component={Login} />
</Router>
<!-- private.routes.svelte -->

<script>
  // @ts-nocheck

  import Header from "./components/layout/header.svelte";
  import Authlayout from "./layout/authlayout.svelte";
  import Routes from "./routes/routes.svelte";
  import Public from "./routes/public.routes.svelte";
  import { currentLayout, getCurrentUser } from "./lib/store/auth";
  import Dashlayout from "./layout/dashlayout.svelte";
  import "@popperjs/core/dist/umd/popper.min.js";
  import "bootstrap/dist/css/bootstrap.min.css";
  import "bootstrap/dist/js/bootstrap.bundle.min.js";
  import { SvelteToast } from '@zerodevx/svelte-toast'


  let user = getCurrentUser();
  let options = {
    position: 'top-right',
    duration: 5000,
    dismissable: true,
    pauseOnHover: true,
    label: 'Close',
    type: 'info',
    animate: {
      in: 'fadeIn',
      out: 'fadeOut'
    }
  }
</script>
<SvelteToast options = {options} />
{#if $currentLayout === "dashboard"}
  <Header />
  <Dashlayout title="Dashboard">
    <Routes {user} />
  </Dashlayout>
{:else}
  <Authlayout title="Login">
    <Public />
    <Routes {user} />
  </Authlayout>
{/if}

<script>
    // @ts-nocheck

    import { link, navigate } from "svelte-routing";
    import { AUTH_LOCAL_STORAGE_KEY } from "../../api/config";
    import { getCurrentUser } from "../../lib/store/auth";
    import { onDestroy, onMount } from "svelte";
    import { routes } from "../../routes/routes";
    import { sideBarToggle } from "../../lib/store/uistore/_toggle";
    import { fade, slide, fly } from "svelte/transition";
    import { spring } from "svelte/motion";

    var user = {};
    var path = window.location.pathname;
    let isDropdownOpen = false;
    let isOpen = false;
    function toggleDropdown() {
        isDropdownOpen = !isDropdownOpen;
    }

    let expandedRoute = null;
    function onActive(route) {
        path = route.path;
    }

    function toggleExpanded(route) {
        if (expandedRoute === route) {
            expandedRoute = null;
        } else {
            expandedRoute = route;
        }
    }
    function onLogout() {
        localStorage.removeItem(AUTH_LOCAL_STORAGE_KEY);
        navigate("/auth/login");
        window.location.reload();
    }
    onMount(() => {
        user = getCurrentUser();
    });
    let showNav = false;
    sideBarToggle.subscribe((val) => {
        showNav = val;
    });
    onDestroy(() => {
        sideBarToggle.set(false);
    });
</script>

{#key showNav}
    <nav
        id="sidebarMenu"
        class="col-md-3 col-lg-2 d-md-block bg-light sidebar collapse mt-5"
        class:show={showNav ? "show" : ""}
        transition:fly={{ y: -100, duration: 600 }}
    >
        <div class="position-sticky pt-3">
            <button
                class="navbar-toggler"
                type="button"
                on:click={toggleDropdown}
            >
                <span class="navbar-toggler-icon"></span>
            </button>
            <ul class="nav flex-column">
                {#each routes as route}
                   

                    {#if route.hasChildren && route.path !== "/auth/login"}
                        <li
                            aria-current={route.path === path
                                ? "page"
                                : undefined}
                            class="nav-item"
                        >
                            <!-- svelte-ignore a11y-click-events-have-key-events -->
                            <!-- svelte-ignore a11y-no-static-element-interactions -->
                            <!-- svelte-ignore a11y-missing-attribute -->
                            <a
                                class={route.path === path
                                    ? "nav-link active"
                                    : "nav-link"}
                                on:click={() => {
                                    toggleExpanded(route);
                                    onActive(route);
                                }}
                            >
                                {route.name}
                            </a>
                        </li>
                    {:else if route.path !== "/auth/login"}
                        <li
                            class="nav-item"
                            aria-current={route.path === path
                                ? "page"
                                : undefined}
                        >
                            <a
                                href={route.path}
                                class={route.path === path
                                    ? "nav-link active"
                                    : "nav-link"}
                                use:link
                                on:click={() => {
                                    onActive(route);
                                    toggleExpanded(route);
                                }}
                            >
                                {route.name}
                            </a>
                        </li>
                    {/if}
                    {#if expandedRoute === route && route.hasChildren}
                        {#each route.children as child}
                            <li
                                class="nav-item"
                                aria-current={child.path === path
                                    ? "page"
                                    : undefined}
                            >
                                <a
                                    href={child.path}
                                    use:link
                                    on:click={() => onActive(child)}
                                    class={child.path === path
                                        ? "nav-link active"
                                        : "nav-link"}
                                    >{child.name}
                                </a>
                            </li>
                        {/each}
                    {/if}
                {/each}
            </ul>
        </div>
    </nav>
{/key}

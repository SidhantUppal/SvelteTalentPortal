<script>
	// @ts-nocheck

	import { onMount } from "svelte";
	import { routes } from "../../routes/routes";
	import Dropdown from "../Dropdown.svelte";
	import { link, links, navigate } from "svelte-routing";
	import { getCurrentUser } from "../../lib/store/auth";
	import { AUTH_LOCAL_STORAGE_KEY } from "../../api/config";
	import { writable } from "svelte/store";
    import { sideBarToggle } from "../../lib/store/uistore/_toggle";

	var user = {};
	var path = window.location.pathname;
	let currentPath = writable(window.location.pathname);
	var talentImg =
		"https://talentone.teamtalentelgia.com/images/sidebar_image.png";
	var talentLogo = "https://talentone.teamtalentelgia.com/images/tt-one.svg";
	let isDropdownOpen = false;
	let isOpen = false;
	function toggleDropdown() {
		isDropdownOpen = !isDropdownOpen;
	}
	function toggleUser() {
		isOpen = !isOpen;
	}
	let expandedRoute = null;

	function toggleExpanded(route) {
		if (expandedRoute === route) {
			expandedRoute = null;
		} else {
			expandedRoute = route;
		}
	}
	onMount(() => {
		user = getCurrentUser();
	});
	function toggleSidebar() {
		sideBarToggle.update((val) => !val);
	}

	const handleDropdownFocusLoss = ({ relatedTarget, currentTarget }) => {
    if (relatedTarget instanceof HTMLElement && currentTarget.contains(relatedTarget)) return // check if the new focus target doesn't present in the dropdown tree (exclude ul\li padding area because relatedTarget, in this case, will be null) 
    isOpen = false
  }	
</script>

<header
	class="navbar navbar-dark sticky-top primary-bg navbar-header flex-md-nowrap p-0 shadow "
>
	<a class="navbar-brand col-md-3 col-lg-2" href="/">
		<img src={talentLogo} alt="TalentOne" class="col-12 navbar-brand-img" />
	</a>
	<button
		class="navbar-toggler position-absolute d-md-none"
		type="button"
		aria-expanded= {$sideBarToggle ? "true" : "false"}
		aria-label="Toggle navigation"
		on:click={toggleSidebar}
		class:collapsed={!$sideBarToggle}
	>
		<span class="navbar-toggler-icon"></span>
	</button>
	<div class="navbar-nav">
		<div class="nav-item text-nowrap">
			<div class="profile-btn d-flex align-items-center p-3">
				<p class="mb-0 text-white me-2">
					{new Date().toLocaleDateString()}
				</p>
				<img src={talentImg} class="me-2" alt="icon" />
				<div
					class="border text-white profile-dropdown position-relative"
					on:focusout={handleDropdownFocusLoss}
				>
					<button
						class="btn primary-bg text-white"
						on:click={() => toggleUser()}
					>
						{user?.username}
					</button>
					{#if isOpen}
						<Dropdown {isOpen} />
					{/if}
				</div>
			</div>
		</div>
	</div>
</header>

<style>
	.primary-bg {
		background-color: #5771f5;
	}
	.profile-btn img {
		width: 30px;
		height: 30px;
		border-radius: 50%;
	}
	.navbar-brand {
		padding: 0;
	}
	.navbar-brand-img {
		/* width: 100px; */
		background-color: white;
	}
</style>

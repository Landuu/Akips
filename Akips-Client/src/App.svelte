<script>
	// https://carbon-components-svelte.onrender.com/
	// https://carbon-icons-svelte.onrender.com/

	import Router from 'svelte-spa-router'
	import Index from "./pages/Index.svelte";
	import NotFound from "./pages/NotFound.svelte";
	import Hello from "./pages/Hello.svelte";
	import Map from './pages/Map.svelte';

	const routes = {
		'/': Index,
		'/hello': Hello,
		'/map': Map,
		'*': NotFound,
	}
</script>

<main>
	<Router {routes} />
</main>
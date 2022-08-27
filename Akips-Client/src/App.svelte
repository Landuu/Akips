<script>
	// https://carbon-components-svelte.onrender.com/
	// https://carbon-icons-svelte.onrender.com/

	import Router from 'svelte-spa-router'
	import Index from "./pages/Index.svelte";
	import NotFound from "./pages/NotFound.svelte";
	import Hello from "./pages/Hello.svelte";
	import { storeSocketStatus } from "./stores";

	const routes = {
		'/': Index,
		'/hello': Hello,
		'*': NotFound,
	}

	const socketUrl = "ws://localhost:9090";
	const socket = new WebSocket(socketUrl);
	socket.addEventListener("open", (e) => {
		console.log("Websocket", "połączony!");
		storeSocketStatus.set(1);
	});
	socket.addEventListener("message", (e) => {
		console.log("Websocket:", e.data);
	});

</script>

<main>
	<Router {routes} />
</main>
<script>
	import LoadingText from "../components/LoadingText.svelte";
	import ObservedLightning from "carbon-icons-svelte/lib/ObservedLightning.svelte";
	import SkillLevel from "carbon-icons-svelte/lib/SkillLevel.svelte";
	import { Button } from "carbon-components-svelte";
	import { socketUrl } from "../stores";
	import { Logger } from "../logger";
	import { onDestroy, onMount } from "svelte";
	import { push } from 'svelte-spa-router'

	const messages = {
		Connecting: "Oczekiwanie na websocket...",
		Connected: "Połączono! Oczekiwanie na rozpoczęcie meczu...",
		Redirecting: "Mecz rozpoczęty! Przekierowywanie na mapę..."
	}
	let statusText = messages.Connecting;
	let needsRetry = false;
	let socket;
	
	function handleWebsocket() {
		socket = new WebSocket(socketUrl);
		handleListeners(socket, true);
	}

	function handleRetry() {
		statusText = messages.Connecting;
		needsRetry = false;
		handleWebsocket();
	}
	
	// Websocket
	function socketOpen(e) {
		Logger.Info("Połączony!", Logger.Sources.Websocket);
		statusText = messages.Connected;
		needsRetry = false;
	}

	function socketClose(e) {
		Logger.Info("Rozłączony!", Logger.Sources.Websocket);
		needsRetry = true;
	}

	function socketMessage(e) {
		const payload = JSON.parse(e.data);
		if(typeof(payload.Map) === 'undefined') return;
		
		statusText = messages.Redirecting;
		setTimeout(() => {
			if(socket.readyState != 1) {
				needsRetry = true;
				return;
			}

			socket.close();
			push("/hello");
		}, 3000)
	}

	function handleListeners(socket, add) {
		if(add) {
			socket.addEventListener("open", socketOpen);
			socket.addEventListener("message", socketMessage);
			socket.addEventListener("close", socketClose);
		} else {
			socket.removeEventListener("open", socketOpen);
			socket.removeEventListener("message", socketMessage);
			socket.removeEventListener("close", socketClose);
		}
	}

	onMount(() => {
		handleWebsocket();
	})

	onDestroy(() => {
		handleListeners(socket, false);
	});
</script>

<div class="container">
	{#if !needsRetry}
		<LoadingText text={statusText} />
	{:else}
		<SkillLevel size={32} />
		<h4>Nie udało się połączyć z serwerem</h4>
		<Button on:click={handleRetry} icon={ObservedLightning}>Spróbuj ponownie</Button>
	{/if}
</div>


<style>
	.container {
		width: 100vw;
        height: 100vh;
        display: flex;
        justify-content: center;
        align-items: center;
        flex-direction: column;
	}

	h4 {
		margin-bottom: 1rem;
	}
</style>
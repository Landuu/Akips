<script>
    import Coords from "../components/Coords.svelte";
    import LoadingText from "../components/LoadingText.svelte";
    import TarkovMap from "../components/TarkovMap.svelte";
    import { socketUrl } from "../stores";
    import { Logger } from "../logger";
    import { replace } from 'svelte-spa-router'
    import { onDestroy } from 'svelte';

    const messages = {
        Waiting: "Oczekiwanie na mapę..."
    }

    let location = {
        Map: null,
        X: null,
        Y: null,
        Z: null
    };

    let loading = false;
    let statusText = ".";
    let socket = new WebSocket(socketUrl);
	handleListeners(socket, true);

	// Websocket
	function socketOpen(e) {
		Logger.Info("Połączony!", Logger.Sources.Websocket);
        statusText = messages.Waiting;
	}

	function socketClose(e) {
		Logger.Info("Rozłączony!", Logger.Sources.Websocket);
        replace("/");
	}

	function socketMessage(e) {
        if(loading) loading = false;
		const payload = JSON.parse(e.data);
		if(typeof(payload.Map) === 'undefined') return;
		
		location = payload;
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

	onDestroy(() => {
		handleListeners(socket, false);
	});
</script>

<div class="container">
    {#if !loading}
        <Coords data={location} />
        <TarkovMap />
    {:else}
        <div class="absolute-center">
            <LoadingText text="{statusText}"/>
        </div>
    {/if}
</div>

<style>
    .absolute-center {
        width: 100vw;
        height: 100vh;
        display: flex;
        justify-content: center;
        align-items: center;
        flex-direction: column;
    }
</style>
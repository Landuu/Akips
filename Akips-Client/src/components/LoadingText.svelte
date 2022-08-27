<script>
	import { Loading } from "carbon-components-svelte";

    export let text = "Loading...";
</script>


<Loading withOverlay={false} />
<h4>{text}</h4>


<style>
    h4 {
        margin-top: 1rem;
    }
</style>
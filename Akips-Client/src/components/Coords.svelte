<script>
    export let data;
</script>

<div class="coords">
    <div class="map"> {data.Map} </div>
    <div class="coord-wrapper">
        <div class="coord-name"> X: </div>
        <div class="coord"> {data.X} </div>
    </div>
    <div class="coord-wrapper">
        <div class="coord-name"> Y: </div>
        <div class="coord"> {data.Y} </div>
    </div>
    <div class="coord-wrapper">
        <div class="coord-name"> Z: </div>
        <div class="coord"> {data.Z} </div>
    </div>
</div>

<style>
    .coords {
        position: absolute;
        left: 1rem;
        top: 1rem;
        z-index: 100;
    }

    .map {
        font-size: x-large;
        margin-bottom: 0.5rem;
    }

    .coord-wrapper {
        display: flex;
        font-size: larger;
    }

    .coord-name {
        margin-left: 0.5rem;
        margin-right: 0.5rem;
    }
</style>
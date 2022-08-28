<script>
    import L from 'leaflet'
    import { onMount } from 'svelte';

    var yx = L.latLng;
    var xy = function(x, y) {
    if (L.Util.isArray(x)) {    // When doing xy([x, y]);
        return yx(x[1], x[0]);
    }
    return yx(y, x);  // When doing xy(x, y);
    };

    

    onMount(() => {
        let map = L.map('map', {
            crs: L.CRS.Simple
        });
        const bounds = [[0, 0], [1000, 1000]];
        const image = L.imageOverlay('https://www.gry-online.pl/static/mapy/pl/gfx/map_2466.jpg', bounds).addTo(map);
        map.fitBounds(bounds);
        L.marker(xy(10, 10)).addTo(map);
    });
</script>

<div id="map" class="tarkov-container">

</div>

<style>
    .tarkov-container {
        width: 100vw;
        height: 100vh;
    }
</style>
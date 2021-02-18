function ZoomControl(div, map) {

    // Get the control DIV. We'll attach our control UI to this DIV.
    var controlDiv = div;

    // Set CSS for the controls. 
    controlDiv.className = "btn actionBtn p-0"

    var zoomout = document.createElement('a');
    zoomout.className = "btn actionBtn m-0";
    zoomout.title = 'Click to zoom out';

    //icon
    var zoomouticon = document.createElement('i');
    zoomouticon.className = "ri-subtract-line";
    zoomout.appendChild(zoomouticon);
    zoomout.append('   ');
    zoomout.append('    |  ');
    controlDiv.appendChild(zoomout);



    //zoom in
    var zoomin = document.createElement('a');
    zoomin.title = 'Click to zoom in';
    zoomin.className = "btn actionBtn m-0";

    //icon
    var zoominicon = document.createElement('i');
    zoominicon.className = "ri-add-fill";
    zoomin.appendChild(zoominicon);
    controlDiv.appendChild(zoomin);

    controlDiv.appendChild(zoomin);


    // Setup the click event listeners for zoom-in, zoom-out:
    google.maps.event.addDomListener(zoomout, 'click', function () {
        var currentZoomLevel = map.getZoom();
        if (currentZoomLevel != 0) {
            map.setZoom(currentZoomLevel - 1);
        }
    });

    google.maps.event.addDomListener(zoomin, 'click', function () {
        var currentZoomLevel = map.getZoom();
        if (currentZoomLevel != 21) {
            map.setZoom(currentZoomLevel + 1);
        }
    });
}
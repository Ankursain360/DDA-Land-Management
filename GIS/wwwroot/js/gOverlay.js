// Define the overlay, derived from google.maps.OverlayView
google.maps.Label = function (opt_options) {
    // Initialization
    this.visible = !0;
    this.setValues(opt_options);
    this.Marker = opt_options.Marker;
    // Label specific
    var span = this.span_ = document.createElement('span');
    span.className = opt_options.cssName || "rLabel";

    //span.style.cssText = 'position: relative; left: -50%; top: -8px; ' +
    //'white-space: nowrap; border: 1px solid blue; ' +
    //'padding: 2px; background-color: white';
    var div = this.div_ = document.createElement('div');
    div.appendChild(span);
    div.style.cssText = 'position: absolute; display: none';
}

google.maps.Label.prototype = new google.maps.OverlayView;

// Implement onAdd
google.maps.Label.prototype.onAdd = function () {
    var pane = this.getPanes().overlayImage;
    pane.appendChild(this.div_);
    var _me = this;
    if (this.Marker)
        google.maps.event.addListener(this.Marker, 'dragend', function () {
            me.set('position', me.Marker.get('position'));
        });

    // Ensures the label is redrawn if the text or position is changed.
    var me = this;
    this.listeners_ = [
      google.maps.event.addListener(this, 'position_changed', function () { me.draw(); }),
      google.maps.event.addListener(this, 'visible_changed', function () { me.draw(); }),
      google.maps.event.addListener(this, 'clickable_changed', function () { me.draw(); }),
      google.maps.event.addListener(this, 'text_changed', function () { me.draw(); }),
      google.maps.event.addListener(this, 'zindex_changed', function () { me.draw(); }),
      google.maps.event.addDomListener(this.div_, 'click', function () {
          if (me.get('clickable')) {
              google.maps.event.trigger(me, 'click');
          }
      })
    ];
};


// Implement onRemove
google.maps.Label.prototype.onRemove = function () {
    this.div_.parentNode.removeChild(this.div_);

    // Label is removed from the map, stop updating its position/text.
    for (var i = 0, I = this.listeners_.length; i < I; ++i) {
        google.maps.event.removeListener(this.listeners_[i]);
    }
};

// Implement draw
google.maps.Label.prototype.draw = function () {
    var projection = this.getProjection();
    var position = projection.fromLatLngToDivPixel(this.get('position'));
    var div = this.div_;
    div.style.left = position.x + 'px';
    div.style.top = position.y + 'px';
    this.visible = (map.getZoom() > this.visibleZoom && map.getZoom() < this.hideZoom);
    div.style.display = this.visible ? 'block' : 'none';
    this.span_.title = this.span_.innerHTML = this.get('text');
};
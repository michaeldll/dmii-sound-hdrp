function checkVolume() {
    if (db > 0.1) {
        this.timeout.clear();
        this.timeout = setTimeout(onSilenceHandler, 1000); 
    }
}
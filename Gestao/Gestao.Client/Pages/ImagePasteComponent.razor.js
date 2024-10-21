window.ImagePasteComponent = {
    init: function (inputFile) {
        document.addEventListener('paste', function (e) {
            if (e.clipboardData) {
                var items = e.clipboardData.items;
                for (var i = 0; i < items.length; i++) {
                    if (items[i].kind === 'file' && items[i].type.indexOf('image') !== -1) {
                        var blob = items[i].getAsFile();
                        var url = URL.createObjectURL(blob);
                        document.getElementById('pastedImage').src = url;
                        console.log(url);
                        var input = inputFile;
                        var dt = new DataTransfer();
                        dt.items.add(blob);
                        input.files = dt.files;
                    }
                }
            }
        });
    },
    setImage: function (imageSrc) {
        document.getElementById('pastedImage').src = imageSrc;
        console.log(imageSrc);
    }
};
$(document).ready(function () {
    var requestAjax = function (endpoint, callback) {
        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                callback(this.responseXML);
            }
        };
        xhr.responseType = "document";
        xhr.open('GET', endpoint, true);
        xhr.send();
    };

    requestAjax("https://office365apieditor.blob.core.windows.net/build?restype=container&comp=list", function (response) {
        var urlTags = response.documentElement.getElementsByTagName("Url");
        var urls = [];

        for (i = 0; i < urlTags.length; i++) {
            if (urlTags[i].textContent.endsWith('.zip')) {
                urls.push(urlTags[i].textContent);
            }
        }

        urls.sort(function (a, b) {
            var versionA = a.match(/\d+\.\d+\.\d+\.\d+/).toString().split('.'); // Major.Minor.Build.Revision
            var versionB = b.match(/\d+\.\d+\.\d+\.\d+/).toString().split('.');

            if (versionA[0] != versionB[0]) {
                return versionB[0] - versionA[0];
            }

            if (versionA[1] != versionB[1]) {
                return versionB[1] - versionA[1];
            }

            if (versionA[2] != versionB[2]) {
                return versionB[2] - versionA[2];
            }

            if (versionA[3] != versionB[3]) {
                return versionB[3] - versionA[3];
            }
            
            return true;
        })

        var buildList = $('#builds')

        for (i = 0; i < urls.length; i++) {
            var url = urls[i];
            var fileName = url.replace(/\g/, "/").replace(/.*\//, '');

            buildList.append('<li><a href=\'' + url + '\' target=_blank>' + fileName + '</a></li>');
          }
    });
});


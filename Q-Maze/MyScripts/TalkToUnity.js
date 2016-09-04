
function SaySomethingToUnity()
{
    document.getElementById("UnityContent").SendMessage("MyObject", "MyFunction", "Hello from a web page!");
    getUnity().SendMessage("MyObject", "MyFunction", "Hello again!");
    alert("???");
}

function detectUnityWebPlayer() {
    var tInstalled = false;
    if (navigator.appVersion.indexOf("MSIE") != -1 &&
        navigator.appVersion.toLowerCase().indexOf("win") != -1) {
        tInstalled = detectUnityWebPlayerActiveX();
    }
    else if (navigator.mimeTypes && navigator.mimeTypes["application/vnd.unity"]) {
        if (navigator.mimeTypes["application/vnd.unity"].enabledPlugin &&
            navigator.plugins && navigator.plugins["Unity Player"]) {
            tInstalled = true;
        }
    }
    return tInstalled;
}


function SayHello(arg)
{
    // show the message
    alert(arg);
}
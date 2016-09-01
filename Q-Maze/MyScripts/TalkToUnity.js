
function SaySomethingToUnity()
{
    document.getElementById("UnityContent").SendMessage("MyObject", "MyFunction", "Hello from a web page!");
    getUnity().SendMessage("MyObject", "MyFunction", "Hello again!");
    alert("???");
}


function SayHello(arg)
{
    // show the message
    alert(arg);
}
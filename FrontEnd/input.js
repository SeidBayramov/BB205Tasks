let input=document.getElementById("text")
let btn=document.querySelector("#btn")
let txt=document.querySelector("#textfile")

btn.addEventListener("click",function()
{
    let li=document.createElement("li")
    li.innerText = input.value
    txt.appendChild(li);
    input.value = "";
})
    
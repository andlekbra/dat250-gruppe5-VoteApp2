var coll = document.getElementsByClassName("collapsible");
var i;

for (i = 0; i < coll.length; i++) {
  coll[i].addEventListener("click", function () {
    this.classList.toggle("active");
    var content = this.nextElementSibling;
    if (content.style.display === "block") {
      content.style.display = "none";
    } else {
      content.style.display = "block";
    }
  });
}
let serverURL = document.getElementById("serverURL").value;
let pollID = document.getElementById("pollID").value;

function testConnection() {
  serverURL = document.getElementById("serverURL").value;
  pollID = document.getElementById("pollID").value;
  console.log("Testing: " + serverURL + " , " + pollID);
  //TODO do some test

  var request = new XMLHttpRequest();

  // Begin accessing JSON data here
  var data = JSON.parse(this.response);

  request.open("GET", serverURL + "/api/v1/OngoingPolls/" + pollID, true);
  request.onload = function () {
    if (request.status >= 200 && request.status < 400) {
      console.log(request.data);
    } else {
      const errorMessage = document.createElement("marquee");
      errorMessage.textContent = `Gah, it's not working!`;
      app.appendChild(errorMessage);
    }
  };
  request.send();
}

function voteGreen() {
  console.log("voted green");
  //TODO
}

function voteRed() {
  //TODO
  console.log("voted red");
}

document.body.addEventListener("keydown", function (e) {
  switch (e.which) {
    case 37: // left
      voteGreen();
      break;

    case 38: // up
      break;

    case 39: // right
      voteRed();
      break;

    case 40: // down
      break;

    default:
      return; // exit this handler for other keys
  }
  e.preventDefault(); // prevent the default action (scroll / move caret)
});

//Fails to compile below this
let socket;

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

  var request = new XMLHttpRequest();

  let requestURL = "https://" + serverURL + "/api/v1/OngoingPolls/" + pollID;
  console.log(requestURL);
  request.open("GET", requestURL, true);

  request.onload = async function () {
    //console.log(request);
    if (request.status >= 200 && request.status < 400) {
      let ongoingPoll = await JSON.parse(request.response);

      console.log(ongoingPoll.data);
      document.getElementById("titleText").textContent =
        ongoingPoll.data.questionTitle;

      document.getElementById("QuestionText").textContent =
        ongoingPoll.data.question;

      document.getElementById("voteGreen").textContent =
        ongoingPoll.data.greenAnswer;
      document.getElementById("voteRed").textContent =
        ongoingPoll.data.redAnswer;
    } else {
      const errorMessage = document.createElement("marquee");
      errorMessage.textContent = `Gah, it's not working!`;
      app.appendChild(errorMessage);
    }
  };
  request.send();

  if (socket) {
    socket.onclose = function () {}; // disable onclose handler first
    socket.close();
    socket = null;
  }
  ///api/v1/OngoingPolls/
  try {
    socket = new WebSocket(
      "wss://" + serverURL + "/api/v1/OngoingPolls/" + pollID + "/live"
    );
    //TODO FIX URL

    socket.addEventListener("open", function (event) {
      socket.send("Websocket Connected");
    });

    socket.addEventListener("message", function (event) {
      console.log("Message from server ", event.data);
      //TODO show voteCount
    });
    //socket.send();
  } catch (ex) {
    console.log(ex);
    console.log("Websocket Failed");
  }
}

function voteGreen() {
  console.log("voted green");

  //TODO
  serverURL = document.getElementById("serverURL").value;
  pollID = document.getElementById("pollID").value;
  console.log("Testing: " + serverURL + " , " + pollID);

  var request = new XMLHttpRequest();

  let requestURL =
    "https://" + serverURL + "/api/v1/OngoingPolls/" + pollID + "/votecounts";
  console.log(requestURL);
  request.open("POST", requestURL, true);
  request.setRequestHeader("Content-Type", "application/json");
  var data = JSON.stringify({
    id: 0,
    createdBy: "empty",
    createdOn: "2021-11-15T20:34:44.735Z",
    lastModifiedBy: "string",
    lastModifiedOn: "2021-11-15T20:34:44.735Z",
    redVotes: 0, //Only this matters
    greenVotes: 1, //Only this matters
  });
  request.send(data);
}

function voteRed() {
  //TODO
  console.log("voted red");

  serverURL = document.getElementById("serverURL").value;
  pollID = document.getElementById("pollID").value;
  console.log("Testing: " + serverURL + " , " + pollID);

  var request = new XMLHttpRequest();

  let requestURL =
    "https://" + serverURL + "/api/v1/OngoingPolls/" + pollID + "/votecounts";
  console.log(requestURL);
  request.open("POST", requestURL, true);
  request.setRequestHeader("Content-Type", "application/json");
  var data = JSON.stringify({
    id: 0,
    createdBy: "empty",
    createdOn: "2021-11-15T20:34:44.735Z",
    lastModifiedBy: "string",
    lastModifiedOn: "2021-11-15T20:34:44.735Z",
    redVotes: 1, //Only this matters
    greenVotes: 0, //Only this matters
  });
  request.send(data);
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

let socket;

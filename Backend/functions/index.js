const functions = require('firebase-functions');
const randomstring = require('randomstring');

// The Firebase Admin SDK to access the Firebase Realtime Database. 
const admin = require('firebase-admin');
admin.initializeApp(functions.config().firebase);

exports.create = functions.https.onRequest((request, response) => {
    var randomStr = randomstring.generate(6);

    var json = {
        "p1" : [
            {
                "x": 0,
                "y": 0
            }
        ]
    }

    admin.database().ref("" + [randomStr]).set(json).then (function() {
        response.send(JSON.stringify({ "id" : randomStr }));
    });
});

exports.join = functions.https.onRequest((request, response) => {
    var randomStr = request.query.q;

    var json = {
        "p1" : [
            {
                "x": 0,
                "y": 0
            }
        ],
        "p2": [
            {
                "x": 0,
                "y": 0
            }
        ]
    }

    var ref = admin.database().ref("" + [randomStr]);
    ref.once('value').then(function(snapshot) {
        if (snapshot.val() == null) {
            response.send(JSON.stringify({ "success": false }));
        }
    });

    ref.set(json).then (function() {
        response.send(JSON.stringify({ "id" : randomStr }));
    });
});

exports.loser = functions.https.onRequest((request, response) => {
    var q = request.query.q;
    var u = request.query.u;

    var json = {
        "loser" : parseInt(u),
    }

    admin.database().ref("" + [q]).set(json).then (function() {
        response.send(json);
    });
});

exports.i = functions.https.onRequest((request, response) => {
    var q = request.query.q;
    var u = Math.abs(1 - request.query.u);
    var i = request.query.i;

    var ref = admin.database().ref("" + [q]);

    ref.once('value').then(function(snapshot) {
        var json = snapshot.val();

        if (i != null && u != null && json != null) {
            var split = i.split(",");
                
            var x = parseFloat(split[0]);
            var y = parseFloat(split[1]);
                
            json[u == 0 ? "p2" : "p1"].push({ "x":x, "y":y });
                
            ref.set(json);
                
            response.redirect("../g?q=" + q + "&u=" + request.query.u);
        }
    });
});

exports.g = functions.https.onRequest((request, response) => {
    var q = request.query.q;
    var u = request.query.u != null ? Math.abs(1 - request.query.u) : null;
    var d = request.query.d;

    var ref = admin.database().ref("" + [q]);

    ref.once('value').then(function(snapshot) {
        var json = snapshot.val();

        if (json != null) {
            if (d == null) {
                if (u == null) {
                    response.send(JSON.stringify(json));
                } else {
                    response.send(JSON.stringify(json[u == 0 ? "p1" : "p2"]));
                }
            } else if (d != null && u != null) {
                var entry = json[u == 0 ? "p1" : "p2"][d];

                if (entry != null) {
                    json[u == 0 ? "p1" : "p2"].splice(d);

                    ref.set(json);

                    response.redirect("../g?q=" + Math.abs(1 - q));
                }
            }
        }

        response.send(JSON.stringify({ "success":false }));
    });
});

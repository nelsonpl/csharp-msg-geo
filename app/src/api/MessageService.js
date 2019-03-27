import firebase from "firebase";
import account from "./AuthService";

export default {
  async save(model) {

    var userUid = account.currentUserUid();
    var userName = account.currentUserName();

    try {
      return firebase.firestore().collection('messages').add({
        type: "Feature",
        properties: {
          type: model.type,
          userId: userUid,
          userName: userName,
          text: model.msg,
          timestamp: firebase.firestore.FieldValue.serverTimestamp()
        },
        geometry: {
          type: "Point",
          coordinates: [model.longitude, model.latitude]
        }
      });
    } catch (error) {
      console.error('Error writing new message to Firebase Database', error);
    }

  },
  get(fnc) {

    var query = firebase.firestore()
      .collection('messages')
      .limit(50);

    query.onSnapshot(function (snapshot) {
      snapshot.docChanges().forEach(function (change) {
        var entity = change.doc.data();
        entity.id = change.doc.id;
        fnc(entity);
      });
    });

    // firebase.firestore().collection("messages").limit(1).get()
    //   .then(function (querySnapshot) {
    //     querySnapshot.forEach(function (doc) {
    //       console.log(doc.id, " => ", doc.data());
    //     });
    //   })
    //   .catch(function (error) {
    //     console.log("Error getting documents: ", error);
    //   });

    // var query = firebase.firestore()
    //   .collection('messages')
    //   .limit(1);

    // query.onSnapshot(function (snapshot) {
    //   snapshot.docChanges().forEach(function (change) {
    //       var message = change.doc.data();
    //       alert(1);
    //       //fnc(message);
    //   });
    // });
  }
};
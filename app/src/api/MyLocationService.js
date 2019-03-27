import firebase from "firebase";
import account from "./AuthService";

export default {
  async save(model) {

    var userUid = account.currentUserUid();
    var userName = account.currentUserName();

    try {
      return firebase.firestore().collection('myLocations').add({
        userUid: userUid,
        title: model.title,
        latitude: model.latitude,
        longitude: model.longitude
      });
    } catch (error) {
      console.error('Error writing new message to Firebase Database', error);
    }

  },

  get(fnc) {

    var query = firebase.firestore()
      .collection('myLocations')
      .limit(50);

    query.onSnapshot(function (snapshot) {
      snapshot.docChanges().forEach(function (change) {
        var entity = change.doc.data();
        entity.id = change.doc.id;
        fnc(entity);
      });
    });
  },

  delete(id){
    firebase.database().ref('myLocations/' + id).remove();
  }
};
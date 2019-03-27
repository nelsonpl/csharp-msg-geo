import firebase from "firebase";

export default {
  currentUserUid() {
    return firebase.auth().currentUser.uid;
  },
  currentUserName() {
    return firebase.auth().currentUser.displayName;
  }
}

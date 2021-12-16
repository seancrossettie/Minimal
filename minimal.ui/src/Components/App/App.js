import { Button } from "@chakra-ui/react";
import { useEffect, useState } from "react";
import firebase from "firebase/compat/app";
import "firebase/compat/auth";
import { signInUser, signOutUser } from "../../helpers/auth";
import { getUserByFirebaseKey } from "../../helpers/data/userData";
import Router from "../../helpers/Router";

function App() {
  const [user, setUser] = useState(null);

  useEffect(() => {
    firebase.auth().onAuthStateChanged((authed) => {
      if (authed) {
        authed.getIdToken().then((token) => sessionStorage.setItem("token", token));
        getUserByFirebaseKey(authed.multiFactor.user.uid).then(setUser);
      } else {
        setUser(false);
      }
    });
  }, []);

  return (
    <div className="App">
      { user
        ? <Button outline onClick={() => signOutUser()}>Sign Out</Button>
        : <Button outline onClick={() => signInUser(setUser)}>Sign In</Button>
      }
      <Router />
    </div>
  );
}

export default App;

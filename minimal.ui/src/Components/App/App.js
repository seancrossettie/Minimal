import { Button } from "@chakra-ui/react";
import { useEffect, useState } from "react";
import firebase from "firebase/compat/app";
import "firebase/compat/auth";
import { signInUser, signOutUser } from "../../helpers/auth";
import { getAllUsers } from "../../helpers/data/userData";

function App() {
  const [user, setUser] = useState(null);

  useEffect(() => {
    firebase.auth().onAuthStateChanged((authed) => {
      if (authed) {
        authed.getIdToken().then((token) => sessionStorage.setItem("token", token));
        setUser(authed);
      } else {
        setUser(false);
      }
    });
    getAllUsers().then(r => console.warn(r));
  }, []);

  return (
    <div className="App">
      { user
        ? <Button outline onClick={() => signOutUser()}>Sign Out</Button>
        : <Button outline onClick={() => signInUser(setUser)}>Sign In</Button>
      }
    </div>
  );
}

export default App;

import { Button } from "@chakra-ui/react";
import { useEffect, useState } from "react";
import firebase from "firebase/compat/app";
import "firebase/compat/auth";
import { signInUser } from "../../helpers/auth";

function App() {
  const [user, setUser] = useState([]);

  useEffect(() => {
    firebase.auth().onAuthStateChanged((authed) => {
      if (authed) {
        authed.getIdToken().then((token) => sessionStorage.setItem("token", token));
        setUser(authed);
      } else {
        setUser(false);
      }
    });
  }, []);

  return (
    <div className="App">
        <Button outline onClick={() => signInUser()}>Sign In</Button>
        <Button outline onClick={() => console.warn(user)}>Test</Button>
    </div>
  );
}

export default App;

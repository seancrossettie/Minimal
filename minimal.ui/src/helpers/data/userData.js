import axios from "axios";
import config from "../config";

const createNewUser = (user) => new Promise((resolve, reject) => {
    axios.post(`${config.baseUrl}/api/users/createUser`, user)
    .then(response => resolve(response.data))
    .catch(error => reject(error));
});

export default createNewUser;


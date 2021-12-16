import axios from "axios";
import config from "../config";

const createNewUser = (user) => new Promise((resolve, reject) => {
    axios.post(`${config.baseUrl}/api/users/createUser`, user)
    .then(response => resolve(response.data)).catch(reject);
});

const getAllUsers = () => new Promise((resolve, reject) => {
    axios.get(`${config.baseUrl}/api/users/getAllUsers`)
    .then(response => resolve(response.data)).catch(reject);
});

const getUserByFirebaseKey = (firebaseKey) => new Promise((resolve, reject) => {
    axios.get(`${config.baseUrl}/api/users/getUserByFirebaseKey/${firebaseKey}`)
    .then(response => resolve(response.data)).catch(reject);
})

export { createNewUser, getAllUsers, getUserByFirebaseKey };


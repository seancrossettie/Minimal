import React from "react";
import { Route, Routes } from "react-router-dom";
import HomePage from "../views/HomePage";

function Router() {
    return (
        <Routes>
            <Route path="/" component={() => <HomePage />} />
        </Routes>
    );
};

export default Router;
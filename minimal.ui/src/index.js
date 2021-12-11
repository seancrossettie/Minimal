import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './Components/App/App.js';
import { ChakraProvider } from '@chakra-ui/react';

ReactDOM.render(
  <React.StrictMode>
    <ChakraProvider>
      <App />
    </ChakraProvider>
  </React.StrictMode>,
  document.getElementById('root')
);


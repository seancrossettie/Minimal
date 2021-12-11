import { Button, ChakraProvider } from "@chakra-ui/react";

function App() {
  return (
    <div className="App">
      <Button outline onClick={() => console.warn("Hello world")}>Test</Button>
    </div>
  );
}

export default App;

import "./App.css";
import { ApiContextProvider, QueryClientProvider } from "./providers";
import { TestArnaud } from "./TestArnaud";

function App() {
  return (
    <QueryClientProvider>
      <ApiContextProvider>
        <TestArnaud />
      </ApiContextProvider>
    </QueryClientProvider>
  );
}

export default App;

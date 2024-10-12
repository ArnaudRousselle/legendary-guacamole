import { GridItem } from "./components/design/GridItem";
import { ApiContextProvider, QueryClientProvider } from "./providers";
import Grid from "@mui/material/Grid2";

function App() {
  return (
    <QueryClientProvider>
      <ApiContextProvider>
        <Grid container spacing={2}>
          <Grid size={8}>
            <GridItem>listing opérations</GridItem>
          </Grid>
          <Grid size={4}>
            <GridItem>résultats</GridItem>
          </Grid>
          <Grid size={7}>
            <GridItem>échéancier</GridItem>
          </Grid>
          <Grid size={5}>
            <GridItem>graphique</GridItem>
          </Grid>
        </Grid>
      </ApiContextProvider>
    </QueryClientProvider>
  );
}

export default App;

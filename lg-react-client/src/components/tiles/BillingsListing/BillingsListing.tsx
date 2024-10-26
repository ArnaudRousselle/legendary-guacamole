import { useQuery } from "@tanstack/react-query";
import { useApi } from "../../../contexts";
import {
  CircularProgress,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Tooltip,
} from "@mui/material";
import { formatShortDate } from "../../../utils/formatShortDate";
import InfoIcon from "@mui/icons-material/Info";
import { CheckedCheckbox } from "./CheckedCheckbox";

export const BillingsListing = () => {
  const { workspaceApi } = useApi();
  const { isFetching, data: billings = [] } = useQuery({
    queryKey: ["billingsListing"],
    queryFn: async () => await workspaceApi.listBillingsQuery({}),
  });

  if (isFetching) return <CircularProgress />;
  return (
    <TableContainer component={Paper}>
      <Table aria-label="billings table">
        <TableHead>
          <TableRow>
            <TableCell>Titre</TableCell>
            <TableCell align="center">Date de valeur</TableCell>
            <TableCell align="right">Montant</TableCell>
            <TableCell align="center">Commentaire</TableCell>
            <TableCell align="center">Pointé</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {billings.map((b) => (
            <TableRow key={b.id}>
              <TableCell>{b.title}</TableCell>
              <TableCell align="center">
                {formatShortDate(b.valuationDate)}
              </TableCell>
              <TableCell align="right">{b.amount.toFixed(2)}</TableCell>
              <TableCell align="center">
                {b.comment ? (
                  <Tooltip title={b.comment}>
                    <InfoIcon fontSize="small" />
                  </Tooltip>
                ) : null}
              </TableCell>
              <TableCell align="center">
                <CheckedCheckbox billingId={b.id} checked={b.checked} />
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

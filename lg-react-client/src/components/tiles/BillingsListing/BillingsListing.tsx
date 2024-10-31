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
import MoreVertIcon from "@mui/icons-material/MoreVert";
import { CheckedCheckbox } from "./CheckedCheckbox";
import { useListBillingsQuery } from "../../../queries/billings";
import { DropDownButton } from "../../design/DropDownButton";

export const BillingsListing = () => {
  const { isFetching, data: billings = [] } = useListBillingsQuery();

  if (isFetching) return <CircularProgress />;
  return (
    <TableContainer component={Paper} sx={{ maxHeight: 440 }}>
      <Table stickyHeader aria-label="billings table">
        <TableHead>
          <TableRow>
            <TableCell align="center"></TableCell>
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
              <TableCell align="center">
                <DropDownButton
                  icon={<MoreVertIcon />}
                  actions={[
                    { text: "Modifier", onClick: () => console.log("edit") },
                    { text: "Supprimer", onClick: () => console.log("delete") },
                  ]}
                />
              </TableCell>
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

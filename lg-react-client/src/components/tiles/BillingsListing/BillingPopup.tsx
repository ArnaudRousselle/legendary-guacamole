import { Box, Button, Modal, Typography } from "@mui/material";
import { BillingPopupArgs } from "./types";
import {
  useAddBillingQuery,
  useDeleteBillingQuery,
  useEditBillingQuery,
} from "../../../queries/billings";

interface IProps {
  args: BillingPopupArgs;
  onClose: () => void;
}

export const BillingPopup = ({ args, onClose }: IProps) => {
  const { mutate: addBilling, isPending: isAddBillingPending } =
    useAddBillingQuery(onClose);
  const { mutate: editBilling, isPending: isEditBillingPending } =
    useEditBillingQuery(onClose);
  const { mutate: deleteBilling, isPending: isDeleteBillingPending } =
    useDeleteBillingQuery(onClose);
  return (
    <Modal open onClose={onClose}>
      <Box>
        <Typography variant="h6" component="h2">
          {args.mode === "create"
            ? "Ajout d'une transaction"
            : args.mode === "edit"
            ? "Modification d'une transaction"
            : args.mode === "delete"
            ? "Suppression d'une transaction"
            : ""}
        </Typography>
        {args.mode === "create" && (
          <>
            <Typography>ajout</Typography>
            <Box>
              <Button
                type="button"
                disabled={isAddBillingPending}
                onClick={() =>
                  addBilling({
                    amount: 666,
                    checked: false,
                    isArchived: false,
                    isSaving: false,
                    title: "test React",
                    valuationDate: {
                      day: 10,
                      month: 6,
                      year: 2025,
                    },
                  })
                }
              >
                Valider
              </Button>
            </Box>
          </>
        )}
        {args.mode === "edit" && (
          <>
            <Typography>éditer</Typography>
            <Button
              type="button"
              disabled={isEditBillingPending}
              onClick={() =>
                editBilling({
                  id: args.billingId,
                  amount: 666,
                  checked: false,
                  isArchived: false,
                  isSaving: false,
                  title: "test React",
                  valuationDate: {
                    day: 10,
                    month: 6,
                    year: 2025,
                  },
                })
              }
            >
              Valider
            </Button>
          </>
        )}
        {args.mode === "delete" && (
          <>
            <Typography>
              Etes-vous sûr de vouloir supprimer cette ligne ?
            </Typography>
            <Box>
              <Button
                type="button"
                disabled={isDeleteBillingPending}
                onClick={() => deleteBilling({ id: args.billingId })}
              >
                Valider
              </Button>
            </Box>
          </>
        )}
      </Box>
    </Modal>
  );
};

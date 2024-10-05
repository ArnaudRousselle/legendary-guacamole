import { Alert } from "@mui/material";
import { ControllerFieldState, FieldError } from "react-hook-form";

interface IProps {
  fieldState: ControllerFieldState;
}

export const ErrorMessage = ({ fieldState: { error } }: IProps) => {
  if (!error) return null;
  return <Alert severity="error">{getErrorMessage(error)}</Alert>;
};

function getErrorMessage(error: FieldError) {
  if (error.message) return error.message;
  switch (error.type) {
    case "required":
      return "Ce champ est obligatoire";
    case "valueAsNumber":
      return "Veuillez saisir un nombre valide";
    case "valueAsDate":
      return "Veuillez saisir une date valide";
    default:
      return "Valeur incorrecte";
  }
}

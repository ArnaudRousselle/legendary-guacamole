import { Box, Modal, Typography } from "@mui/material";
import { PropsWithChildren, ReactNode } from "react";

interface IProps extends PropsWithChildren {
  title: string;
  onClose: () => void;
  width?: number;
  open?: boolean;
  footer?: ReactNode | undefined;
}

export const Popup = ({
  title,
  children,
  onClose,
  width = 400,
  open = true,
  footer,
}: IProps) => {
  return (
    <Modal open={open} onClose={onClose}>
      <Box
        style={{
          position: "absolute",
          top: "50%",
          left: "50%",
          transform: "translate(-50%, -50%)",
          width,
          border: "2px solid #000",
        }}
        bgcolor="background.paper"
        p={3}
      >
        <Typography variant="h6" component="h2" mb={2}>
          {title}
        </Typography>
        {children}
        {footer !== undefined && <Box textAlign="right">{footer}</Box>}
      </Box>
    </Modal>
  );
};

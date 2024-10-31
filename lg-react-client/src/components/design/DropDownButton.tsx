import { IconButton, Menu, MenuItem } from "@mui/material";
import { useRef, useState } from "react";

interface IProps {
  icon: JSX.Element;
  actions: Array<{ text: string; onClick: () => void }>;
}

export const DropDownButton = ({ icon, actions }: IProps) => {
  const anchor = useRef<HTMLButtonElement>(null);
  const [open, setOpen] = useState(false);
  return (
    <>
      <IconButton ref={anchor} onClick={() => setOpen((p) => !p)}>
        {icon}
      </IconButton>
      {open && (
        <Menu
          id="basic-menu"
          anchorEl={anchor.current}
          open
          onClose={() => setOpen(false)}
          MenuListProps={{
            "aria-labelledby": "basic-button",
          }}
        >
          {actions.map((a, i) => (
            <MenuItem
              key={i}
              onClick={() => {
                a.onClick();
                setOpen(false);
              }}
            >
              {a.text}
            </MenuItem>
          ))}
        </Menu>
      )}
    </>
  );
};

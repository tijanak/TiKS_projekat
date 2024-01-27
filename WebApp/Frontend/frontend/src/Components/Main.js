import React, { useState, useEffect } from "react";
import BACKEND from "../config";
import Card from "@mui/material/Card";
import CardActions from "@mui/material/CardActions";
import CardContent from "@mui/material/CardContent";
import CardMedia from "@mui/material/CardMedia";
import Typography from "@mui/material/Typography";
import Button from "@mui/material/Button";
import { Container } from "@mui/material";
import { useNavigate } from "react-router-dom";
import DeleteIcon from "@mui/icons-material/Delete";
import { useAuth } from "../App";
import Dialog from "@mui/material/Dialog";
import ListItemText from "@mui/material/ListItemText";
import ListItemButton from "@mui/material/ListItemButton";
import List from "@mui/material/List";
import Divider from "@mui/material/Divider";
import Toolbar from "@mui/material/Toolbar";
import IconButton from "@mui/material/IconButton";
import CloseIcon from "@mui/icons-material/Close";
import Slide from "@mui/material/Slide";
import EditIcon from "@mui/icons-material/Edit";
import Fab from "@mui/material/Fab";
import { AppBar } from "@mui/material";
import EditSlucaj from "./EditSlucaj";
function Main() {
  const [posts, setPosts] = useState([]);
  const [reload, setReload] = useState(false);
  const [open, setOpen] = React.useState(false);
  const [editSlucaj, setEditSlucaj] = useState(null);
  const handleClickOpen = (p) => {
    setEditSlucaj(p);
    setOpen(true);
  };

  const handleClose = () => {
    setReload(!reload);
    setOpen(false);
    setEditSlucaj(null);
  };
  useEffect(() => {
    fetch(`${BACKEND}Slucaj/Get/All`, {
      method: "GET",
    })
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        setPosts(data);
      })
      .catch((error) => console.log(error));
  }, [reload]);
  let navigate = useNavigate();

  return (
    <Container>
      {editSlucaj && (
        <EditSlucaj
          close={() => {
            handleClose();
          }}
          open={open}
          p={editSlucaj}
        />
      )}
      <Button variant="outlined" onClick={() => navigate("/dodaj_slucaj")}>
        Dodaj slucaj
      </Button>
      <h2>
        Svi slucajevi(<span id="all-posts">{posts.length}</span>):
      </h2>
      {posts && (
        <>
          {posts.map((p) => (
            <Card
              id={"_" + p.id}
              className="post-card"
              key={p.id}
              sx={{ maxWidth: 345 }}
            >
              <CardActions>
                <Button
                  size="small"
                  onClick={() => {
                    navigate("/doniraj", { state: { id_posta: p.id } });
                  }}
                >
                  Doniraj
                </Button>

                {/*<Button size="small">Udomi</Button>*/}
                <DeleteIcon
                  className="delete-btn"
                  style={{ cursor: "pointer" }}
                  onClick={() => {
                    fetch(`${BACKEND}Slucaj/Delete/${p.id}`, {
                      method: "DELETE",
                    }).finally(() => setReload(!reload));
                  }}
                ></DeleteIcon>
                <Fab
                  onClick={() => {
                    handleClickOpen(p);
                  }}
                  color="secondary"
                  size="small"
                  aria-label="edit"
                >
                  <EditIcon />
                </Fab>
              </CardActions>
              <CardMedia
                height="140"
                alt="stock"
                image={p.slike.length == 0 ? "imgs/stockphoto.jpg" : p.slike[0]}
                title="slika"
                component="img"
              />
              <CardContent>
                <Typography gutterBottom variant="h5" component="div">
                  {p.naziv}
                </Typography>
                <Typography variant="body2" color="text.secondary">
                  {p.opis}
                </Typography>
              </CardContent>
              <CardActions>
                <Button
                  size="small"
                  onClick={() => {
                    navigate("/post", { state: { post: p } });
                  }}
                >
                  Novosti
                </Button>
              </CardActions>
            </Card>
          ))}
        </>
      )}
    </Container>
  );
}
export default Main;

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
function Main() {
  const [posts, setPosts] = useState(null);
  const [reload, setReload] = useState(false);
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
      <Button variant="outlined" onClick={() => navigate("/dodaj_slucaj")}>
        Dodaj slucaj
      </Button>
      <h2>Svi slucajevi({posts && posts.length}):</h2>
      {posts && (
        <>
          {posts.map((p) => (
            <Card key={p.id} sx={{ maxWidth: 345 }}>
              <CardActions>
                <Button size="small" onClick={() => {
                    navigate("/doniraj", { state: { id_posta: p.id } });
                  }}>Doniraj</Button>

                <Button size="small">Udomi</Button>
                <DeleteIcon
                  onClick={() => {
                    fetch(`${BACKEND}Slucaj/Delete/${p.id}`, {
                      method: "DELETE",
                    }).finally(() => setReload(!reload));
                  }}
                ></DeleteIcon>
              </CardActions>
              <CardMedia
                height="140"
                alt="stock"
                image="\imgs\stockphoto.jpg"
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

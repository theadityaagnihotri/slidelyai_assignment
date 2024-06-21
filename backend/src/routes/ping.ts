import { Router } from 'express';

const router = Router();

router.get('/', (req, res) => {
    res.json(true);
});

export default router;

import { Router } from 'express';
import fs from 'fs';

const router = Router();
const dbFilePath = './db.json';

router.get('/', (req, res) => {
    const index = parseInt(req.query.index as string, 10);

    if (isNaN(index) || index < 0) {
        return res.status(400).json({ error: 'Invalid index parameter' });
    }

    const dbData = JSON.parse(fs.readFileSync(dbFilePath, 'utf-8'));
    const submission = dbData.submissions[index];

    if (!submission) {
        return res.status(404).json({ error: 'Submission not found' });
    }

    res.json(submission);
});

export default router;
